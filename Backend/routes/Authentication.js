const mongoose = require('mongoose');

const Account = mongoose.model('adarleatomosCollection');

const Cursos = mongoose.model('adarleatomosCursos');


module.exports = app => {


    app.get('/login/:user/:pass', async(req,res) =>{
        const user = req.params.user;
        const pass = req.params.pass;
        if (user == null || pass == null){
            res.status('409').send('Credenciales invalidas');
            return;
        }

        var userAccount = await Account.findOne({username: user});
        console.log(userAccount);
        if (userAccount == null){
            res.status('404').send('Credenciales invalidas');;//Credenciales invalidas
            return;
        }else{
            if (pass == userAccount.password){
                res.status(200).send(userAccount);
                userAccount.lastAuth = Date.now();
                await userAccount.save();
                return;
            }else{
                res.status('404').send('Credenciales invalidas');;//Credenciales invalidas
                return;
            }
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
        console.log(userAccount);
        if (userAccount == null){
            var newAccount = new Account({
                username: user,
                password: pass,
                progress: 0,
                type: "P",

                lastAuth: Date.now(),
            });
            await newAccount.save();

            res.status('201').send('Usuario Creado');
            return;
        }else{
            res.status('409').send('Usuario ya existe');
            return
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

                lastAuth: Date.now(),
            });
            await newAccount.save();

            res.status('201').send('Usuario Creado');
            console.log('Usuario Creado');
            console.log(newAccount);
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
            console.log('User exists');
            try {
                if (userToDelete.type == "P"){
                    await Cursos.deleteMany({teacher: userToDelete.username})

                    for (let i = 0; i < userToDelete.curso.length; i++) {
                        const curso = userToDelete.curso[i];
                        // Realiza la actualizaci贸n que necesites, por ejemplo, agregar o eliminar
                        await Account.updateMany(
                            { curso: curso},
                            { $pull: { curso: curso }},
                            { new: true, runValidators: true }
                        );
                    }
                    await Account.deleteOne({ username: user });
                }else{
                    await Cursos.findOneAndUpdate(
                        { students: userToDelete.username},
                        { $pull: { students: userToDelete.username} },
                        { new: true, runValidators: true }
                    );
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
        console.log(Findcurso);
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
            return
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
            const studentAccont = await Account.findOne({username: student},);
            if (studentAccont == null){
                res.status(404).send(student +" no encontrado");
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
        console.log(getCurso);
        if (getCurso == null){
            res.status('404').send('Curso no encontrado');
            return;
        }else{
            res.status('200').send(getCurso.students);
        }
    });

    app.get('/student/:user/prog', async(req,res) =>{
        const user = req.params.user;
        var getAccount = await Account.findOne({username: user});
        console.log(getAccount);
        if (getAccount == null){
            res.status('404').send('Usuario no encontrado');
            return;
        }else{
            res.status('200').send(String(getAccount.progress));
        }
    });
}