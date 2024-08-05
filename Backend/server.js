const express = require('express');
const cors = require('cors');
const keys = require('./config/keys.js');

const app = express();
app.use(cors()); 
//Setting Database
const mongoose = require('mongoose');
mongoose.connect( keys.mongoURI, {useNewUrlParser: true, useUnifiedTopology: true});
//Setting DB models (accounts basically)
require('./model/adarleatomosCollection.js');

app.use(express.json());
/*const Account = mongoose.model('adarleatomosCollection');



app.get('/account', async(req,res) =>{
    const {user, pass} = req.query;
    if (user == null || pass == null){
        res.send('Credenciales invalidas');
        return;
    }

    var userAccount = await Account.findOne({username: user});
    console.log(userAccount);
    if (userAccount == null){
        var newAccount = new Account({
            username: user,
            password: pass,
            progress: 0,

            lastAuth: Date.now(),
        });
        await newAccount.save();

        res.send(newAccount);
        return;
    }else{
        if (pass == userAccount.password){
            res.send('Usuario encontrado');
            userAccount.lastAuth = Date.now();
            await userAccount.save();

            res.send(userAccount);
            return;
        }
    }
})
*/
require('./routes/Authentication')(app);
const port = 13756;

app.listen(keys.port, () => {
    console.log('Listening on ' + port);
})