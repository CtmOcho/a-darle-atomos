const mongoose = require('mongoose');

const Account = mongoose.model('adarleatomosCollection');

const Cursos = mongoose.model('adarleatomosCursos');

const Quiz = mongoose.model('adarleatomosTests');


module.exports = app => {

    app.post('/login', async(req, res) => {
        const { user, pass } = req.body; // Obtener usuario y contraseña del cuerpo de la solicitud
    
        if (user == null || pass == null){
            res.status(409).send('Credenciales inválidas');
            return;
        }
    
        try {
            var userAccount = await Account.findOne({username: user});
            
            if (userAccount == null){
                res.status(404).send('Credenciales inválidas');
                return;
            } else {
                if (pass === userAccount.password) {
                    res.status(200).send(userAccount);
                    userAccount.lastAuth = Date.now();
                    await userAccount.save();
                    return;
                } else {
                    res.status(404).send('Credenciales inválidas');
                    return;
                }
            }
        } catch (err) {
            console.error('Error en el proceso de login:', err);
            res.status(500).send('Error interno del servidor');
        }
    });

    

app.post('/teacher/:user/:pass', async(req, res) => {
    const user = req.params.user;
    const pass = req.params.pass;
    if (user == null || pass == null){
        res.status('409').send('Credenciales invalidas');
        return;
    }
    var userAccount = await Account.findOne({username: user});
    
    if (userAccount == null){
        var newAccount = new Account({
            username: user,
            password: pass,
            progress: 0,
            type: "P",
            //progressdata:[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
            lastAuth: Date.now(),
        });
        await newAccount.save();
        var quizAccount = await Quiz.findOne({username: user});
        console.log(quizAccount);
        if(quizAccount == null){
            var newQuiz = new Quiz({
                username: user,
                /*quiz1: [0,0,0,0,0,0],
                quiz2: [0,0,0,0,0,0],
                quiz3: [0,0,0,0,0,0],
                quiz4: [0,0,0,0,0,0],
                quiz5: [0,0,0,0,0,0],
                quiz6: [0,0,0,0,0,0],
                quiz7: [0,0,0,0,0,0],
                quiz8: [0,0,0,0,0,0],
                quiz9: [0,0,0,0,0,0],
                quiz10: [0,0,0,0,0,0],
                quiz11: [0,0,0,0,0,0]*/
            });
            await newQuiz.save();
        }

        res.status('201').send('Usuario Creado');
        return;
    }else{
        res.status('409').send('Usuario ya existe');
        return;
    }
});

app.post('/student', async (req, res) => {
    const {user, pass} = req.query;
    if (user == null || pass == null){
        res.status('409').send('Credenciales invalidas');
        return;
    }
    var userAccount =  await Account.findOne({username: user});
    if (userAccount == null){
        var newAccount = new Account({
            username: user,
            password: pass,
            progress: 0,
            type: "E",
            //progressdata:[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],

            lastAuth: Date.now(),
        });
        await newAccount.save();

        var quizAccount = await Quiz.findOne({username: user});
        console.log(quizAccount);
        if(quizAccount == null){
            var newQuiz = new Quiz({
                username: user,
                /*quiz1: [0,0,0,0,0,0],
                quiz2: [0,0,0,0,0,0],
                quiz3: [0,0,0,0,0,0],
                quiz4: [0,0,0,0,0,0],
                quiz5: [0,0,0,0,0,0],
                quiz6: [0,0,0,0,0,0],
                quiz7: [0,0,0,0,0,0],
                quiz8: [0,0,0,0,0,0],
                quiz9: [0,0,0,0,0,0],
                quiz10: [0,0,0,0,0,0],
                quiz11: [0,0,0,0,0,0]*/
            });
            await newQuiz.save();
        }
        

        res.status('201').send('Usuario Creado');
        console.log('Usuario Creado');
        
        return;
    }else{
        res.status('409').send('Usuario ya existe');
        console.log('Usuario ya existe');

        return;
    }
});

app.put('/updateStudent/:user',async(req,res) => {
    const search = req.params.user;
    const {user,pass} = req.body;

    const updateFields = {};
    if (user !== null) updateFields.username = user;
    if (pass !== null) updateFields.password = pass;

    try {
        
        const updateUser = await Account.findOneAndUpdate(
            { username: search },
            {$set: updateFields },
            { new: true, runValidators: true }
        );

        if (!updateUser) {
            res.status(404).send('Usuario no encontrado');
            return;
        }
        updateFields.students = updateFields.username;
        await Cursos.findOneAndUpdate(
            {students: search},
            {$set: {"students.$":updateFields.students}},
            { new: true, runValidators: true }
        );
        await Quiz.findOneAndUpdate(
            {username: search},
            {username: updateFields.username},
            { new: true, runValidators: true }
        );

        res.status(200).send(updateUser);
    } catch (err) {
        console.error(err);
        res.status(500).send('Error al actualizar el usuario');
    }
});

app.put('/updateStudent/:user/prog',async(req,res) => {
    const search = req.params.user;
    try {
        const updateUser = await Account.findOneAndUpdate(
            { username: search },
            { $inc: {progress: 1} },
            { new: true, runValidators: true }
        );
        if (!updateUser) {
            res.status(404).send('Usuario no encontrado');
            return;
        }
        res.status(200).send(updateUser);
    } catch (err) {
        console.error(err);
        res.status(500).send('Error al actualizar el usuario');
    }
});

app.delete('/student/:user', async (req, res) => {
    const user = req.params.user;
    const userToDelete = await Account.findOne({username: user});
    if(!userToDelete){
        res.status('404').send('Usuario no existe');//Usuario no existe
        return;
    }else{
        console.log('Usuario existe');
        try {
            if (userToDelete.type == "P"){
                await Cursos.updateMany({ teacher: userToDelete.username},
                    { $pull: { teacher: userToDelete.username} },
                    { new: true, runValidators: true }
                );
                const emptyArrays = await Cursos.deleteMany({ teacher: { $size: 0 } });

                if (emptyArrays.deletedCount  > 0) {
                    console.log('Se encontraron arrays vacíos.', emptyArrays);
                    for (let i = 0; i < userToDelete.curso.length; i++) {
                        const curso = userToDelete.curso[i];
                        // Realiza la actualizaci贸n que necesites, por ejemplo, agregar o eliminar
                        await Account.updateMany(
                            { curso: curso},
                            { $pull: { curso: curso }},
                            { new: true, runValidators: true }
                        );
                    }
                    await Quiz.deleteOne({username: user});
                    await Account.deleteOne({ username: user });
                } else {
                    console.log('No se encontraron arrays vacíos.');
                }
                await Account.deleteOne({ username: user });
            }else{
                await Cursos.findOneAndUpdate(
                    { students: userToDelete.username},
                    { $pull: { students: userToDelete.username} },
                    { new: true, runValidators: true }
                );
                await Quiz.deleteOne({username: user});
                await Account.deleteOne({ username: user });
            }

            res.status(200).send('Usuario Eliminado');
        } catch (err) {
            console.error(err);
            res.status(500).send('Error al eliminar el usuario');
        }
        return;
    }
    
});

app.post('/curso/:teacher/:course', async (req,res) =>{
    teacher = req.params.teacher;
    curso = req.params.course;
    if (curso == null || teacher == null){
        res.status(500).send('Data invalida');
        return;
    }
    const userAccount = await Account.findOne({username: teacher, type: 'P'});
    if(userAccount == null){
        res.status(404).send('Profesor no existe');
        return;
    }
    var Findcurso =  await Cursos.findOne({course: curso});

    if (Findcurso == null){
        var newCurso = new Cursos({
            teacher: teacher,
            course: curso,

            lastUpd: Date.now(),
        });
        await newCurso.save();
        
        await Account.findOneAndUpdate(
            {username: teacher},
            {$push: { curso: curso }},
            { new: true, runValidators: true }
        );


        res.status('201').send('Curso Creado');
        return;
    }else{
        res.status('409').send('Curso ya existe');
        return;
    }
});

app.put('/updateCurso/:course/insertStudents',async(req,res) => {
    const course = req.params.course;

    const { students } = req.body;
    if (!Array.isArray(students)) {
        res.status(400).send('El campo "students" debe ser un array');
        return;
    }
    for (let i = 0; i < students.length; i++) {
        const student = students[i];
        // Realiza la actualizaci贸n que necesites, por ejemplo, agregar o eliminar
        const studentAccount = await Account.findOne({username: student},);
        if (studentAccount == null){
            res.status(404).send(student +" no encontrado");
            return;
        }
        if (studentAccount.curso != null){
            const course_act = studentAccount.curso[0];
            await Cursos.findOneAndUpdate(
                { course: course_act },
                { $pull: { students: student} },
                { new: true, runValidators: true }
            );
           
            const updateCourse  = await Account.findOneAndUpdate(
                {username: student},
                { $pull: { curso: course_act }},
                { new: true, runValidators: true }
            );
            if (!updateCourse) {
                res.status(404).send('Curso no encontrado');
                return;
            }
            
        }
    }

    try {
        const updatedCourse = await Cursos.findOneAndUpdate(
            { course: course },
            { $push: { students: { $each: students } } },
            { new: true, runValidators: true }
        );

        if (!updatedCourse) {
            res.status(404).send('Curso no encontrado');
            return;
        }
        for (let i = 0; i < students.length; i++) {
            const student = students[i];
            // Realiza la actualizaci贸n que necesites, por ejemplo, agregar o eliminar
            await Account.findOneAndUpdate(
                {username: student},
                {$push: { curso: course }},
                { new: true, runValidators: true }
            );
        }

        res.status(200).send('Estudiantes insertados en el curso');
    } catch (err) {
        console.error(err);
        res.status(500).send('Error al actualizar el curso');
    }
});

app.put('/updateCurso/:course/removeStudents',async(req,res) => {
    const course = req.params.course;

    const { students } = req.body;
    if (!Array.isArray(students)) {
        res.status(400).send('El campo "students" debe ser un array');
        return;
    }

    for (let i = 0; i < students.length; i++) {
        const student = students[i];
        // Realiza la actualizaci贸n que necesites, por ejemplo, agregar o eliminar
        const studentAccont = await Account.findOne({username: student},);
        if (studentAccont == null){
            res.status(404).send(student +" no encontrado");
            return;
        }
    }

    try {
        const updatedCourse = await Cursos.findOneAndUpdate(
            { course: course },
            { $pull: { students: { $in: students } } },
            { new: true, runValidators: true }
        );

        if (!updatedCourse) {
            res.status(404).send('Curso no encontrado');
            return;
        }

        for (let i = 0; i < students.length; i++) {
            const student = students[i];
            // Realiza la actualizaci贸n que necesites, por ejemplo, agregar o eliminar
            await Account.findOneAndUpdate(
                {username: student},
                {$pull: { curso: course }},
                { new: true, runValidators: true }
            );
        }

        res.status(200).send('Estudiantes eliminados del curso');
    } catch (err) {
        console.error(err);
        res.status(500).send('Error al actualizar el curso');
    }
});

app.delete('/curso/:course', async (req, res) => {
    const curso = req.params.course;
    const cursoToDelete = await Cursos.findOne({course: curso});
    if(!cursoToDelete){
        res.status('404').send('Curso no existe');//Usuario no existe
        return;
    }else{
        console.log('Curso existe');
        try {
            await Cursos.deleteOne({ course: curso });
            const result = await Account.updateMany(
                { curso: curso },
                { $pull: { curso: curso} }
            );
            res.status(200).send('Curso Eliminado');
        } catch (err) {
            console.error(err);
            res.status(500).send('Error al eliminar el curso');
        }
        return;
    }
});

app.get('/curso/students/:course', async(req,res) =>{
    const curso = req.params.course;
    var getCurso = await Cursos.findOne({course: curso});
    
    if (getCurso == null){
        res.status('404').send('Curso no encontrado');
        return;
    }else{
        res.status('200').send(getCurso.students);
    }
});

app.get('/student/:user/prog', async (req, res) => {
    const user = req.params.user;
    var getAccount = await Account.findOne({ username: user });

    if (getAccount == null) {
        res.status(404).send('Usuario no encontrado');
        return;
    } else {
        // Asegurarse de que progressdata existe y es un array
        if (Array.isArray(getAccount.progressdata)) {
            const progressSum = getAccount.progressdata.reduce((acc, val) => acc + val, 0);
            res.status(200).send(String(progressSum));
        } else {
            res.status(500).send('Error: progressdata no es un array');
        }
    }
});

app.get('/curso/:teacher', async (req, res) => {
    const teacher = req.params.teacher;
    const userAccount = await Account.findOne({ username: teacher, type: 'P' });
    if (!userAccount) {
        res.status(404).send('Profesor no existe');
        return;
    }
    const cursos = await Cursos.find({ teacher });
    res.status(200).json(cursos);
    });

    app.get('/students/not-in-course/:course', async (req, res) => {
    const courseName = req.params.course;
    try {
        const course = await Cursos.findOne({ course: courseName });
        if (!course) {
        res.status(404).send('Curso no encontrado');
        return;
        }
        const studentsInCourse = course.students;
        const studentsNotInCourse = await Account.find({
        type: 'E',
        username: { $nin: studentsInCourse }
        });
        res.status(200).json(studentsNotInCourse);
    } catch (error) {
        console.error('Error al obtener los estudiantes:', error);
        res.status(500).send('Error al obtener los estudiantes');
    }
    });

    app.get('/students/in-course/:course', async (req, res) => {
    const courseName = req.params.course;
    try {
        const course = await Cursos.findOne({ course: courseName });
        if (!course) {
        res.status(404).send('Curso no encontrado');
        return;
        }
        const studentsInCourse = course.students;
        const students = await Account.find({
        type: 'E',
        username: { $in: studentsInCourse }
        });
        res.status(200).json(students);
    } catch (error) {
        console.error('Error al obtener los estudiantes:', error);
        res.status(500).send('Error al obtener los estudiantes');
    }
    });

    app.get('/test', (req, res) => {
    res.send('Ngrok is working');
});

app.put('/updateStudent/:user/prog/:progressvalue', async (req, res) => {
    const search = req.params.user;
    const progressIndex = req.params.progressvalue - 1;

    if (progressIndex < 0 || progressIndex >= 50) {
        res.status(400).send('Valor de progressvalue inválido');
        return;
    }

    try {
        const updateUser = await Account.findOneAndUpdate(
            { username: search },
            { $set: { [`progressdata.${progressIndex}`]: 1 } },
            { new: true, runValidators: true }
        );
        if (!updateUser) {
            res.status(404).send('Usuario no encontrado');
            return;
        }
        res.status(200).send(updateUser);
    } catch (err) {
        console.error(err);
        res.status(500).send('Error al actualizar el usuario');
    }
});

app.get('/getStudent/:user/prog/:progressvalue', async (req, res) => {
    const search = req.params.user;
    const progressIndex = req.params.progressvalue - 1;
    //console.log("DENTRO DEL GET LOL");
    //console.log(search);
    //console.log(progressIndex);

    try {
        const user = await Account.findOne({ username: search });
        if (!user) {
            res.status(404).send('Usuario no encontrado');
            return;
        }
        //console.log("ANTES DEL SEND");    
        //console.log(user.progressdata[progressIndex]);
        const progressValue = user.progressdata[progressIndex];
        res.status(200).send({"progressValue": progressValue});
    } catch (err) {
        console.error(err);
        res.status(500).send('Error al obtener el progreso del usuario');
    }
});

app.get('/quiz/:user/:test', async (req, res) => {
    const user = req.params.user;
    const test = req.params.test;
    const studentSearch = await Quiz.findOne({username: user});
    if (studentSearch == null){
        res.status(404).send('Usuario no encontrado');
        return;
    }
    //console.log(test);
    //console.log(studentSearch);
    res.status(200).send(studentSearch[test]);
});

app.put('/updateQuiz/:user/:test/:type/:values', async (req, res) => {
    const search = req.params.user;
    const quiz = req.params.test;
    const type = req.params.type;
    const values = req.params.values.split('').map(Number); // Convertimos la cadena de valores en un array de números

    if (values.length !== 3 || !values.every(v => v === 0 || v === 1)) {
        res.status(400).send('Valores inválidos. Deben ser tres números binarios (0 o 1).');
        return;
    }

    // Definir los índices para pre y post quiz
    const startIndex = type === 'pre' ? 0 : 3;

    try {
        // Generar el objeto de actualización basado en los valores recibidos
        const set = {};
        for (let i = 0; i < 3; i++) {
            set[`${quiz}.${startIndex + i}`] = values[i];
        }

        const updateUser = await Quiz.findOneAndUpdate(
            { username: search },
            { $set: set },
            { new: true, runValidators: true }
        );

        if (!updateUser) {
            res.status(404).send('Usuario no encontrado');
            return;
        }

        res.status(200).send(updateUser[quiz]);
    } catch (err) {
        console.error(err);
        res.status(500).send('Error al actualizar el usuario');
    }
});


// Ruta GET para obtener el array progressdata de un usuario
app.get('/getProgressData/:username', async (req, res) => {
const { username } = req.params;

try {
    // Busca el usuario por su nombre de usuario
    const user = await Account.findOne({ username });

    // Si el usuario no es encontrado, retorna un error 404
    if (!user) {
        return res.status(404).json({ message: 'Usuario no encontrado' });
    }

    // Retorna el array progressdata
    return res.status(200).json({ progressdata: user.progressdata });
} catch (err) {
    console.error('Error al obtener el progreso del usuario:', err);
    return res.status(500).json({ message: 'Error interno del servidor' });
}
});


    
}