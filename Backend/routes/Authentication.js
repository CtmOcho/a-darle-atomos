const mongoose = require('mongoose');

const Account = mongoose.model('adarleatomosCollection');


module.exports = app => {

    app.get('/login/:user/:pass', async(req,res) =>{
        const user = req.params.user;
        const pass = req.params.pass;
        if (user == null || pass == null){
            res.status('404').send('Credenciales invalidas');
            return;
        }

        var userAccount = await Account.findOne({username: user});
        console.log(userAccount);
        if (userAccount == null){
            res.status('404').send('Credenciales invalidas');;//Credenciales invalidas
            return;
        }else{
            if (pass == userAccount.password){
                res.status('200').send(userAccount);
                userAccount.lastAuth = Date.now();
                await userAccount.save();
                //res.send(userAccount);
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
        }
    });

    app.post('/student', async (req, res) => {
        const {user, pass} = req.query;
        var userAccount =  await Account.findOne({username: user});
        console.log(userAccount);
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
            return;
        }else{
            res.status('404').send('Usuario ya existe');
            return
        }
    });

    /*app.put('/cursos/:user',async(req,res) => {


    });*/

    app.delete('/student/:user', async (req, res) => {
        const user = req.params.user;
        const userToDelete = await Account.findOne({username: user});
        if(!userToDelete){
           res.send('404');//Usuario no existe
           return;
        }else{
            console.log('User exists');
            try {
                await Account.deleteOne({ username: user });
                res.status(200).send('Usuario Eliminado');
            } catch (err) {
                console.error(err);
                res.status(500).send('Error al eliminar el usuario');
            }
            return;
        }
       
    });

}