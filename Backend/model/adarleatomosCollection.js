const mongoose = require('mongoose');
const { Schema } = mongoose;

const accountSchema = new Schema({
    username: String,
    password: String,
    progress: Number,
    type: String,
    curso: Array,

    lastAuth: Date,
});

const cursosSchema = new Schema({
    teacher: String,
    students: {type: Array, default: []},
    course: String,

    lastUpd: Date,
});



mongoose.model('adarleatomosCursos', cursosSchema);

mongoose.model('adarleatomosCollection', accountSchema);

