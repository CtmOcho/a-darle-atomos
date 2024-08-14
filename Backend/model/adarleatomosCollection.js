const mongoose = require('mongoose');
const { Schema } = mongoose;

const accountSchema = new Schema({
    username: String,
    password: String,
    progress: Number,
    type: String,
    curso: Array,
    progressdata: Array,
    lastAuth: Date,
});

const cursosSchema = new Schema({
    teacher: String,
    students: {type: Array, default: []},
    course: String,

    lastUpd: Date,
});

const testSchema = new Schema({
    username: String,
    quiz1: {type: Array, default: [0,0,0,0,0,0]},
    quiz2: {type: Array, default: [0,0,0,0,0,0]},
    quiz3: {type: Array, default: [0,0,0,0,0,0]},
    quiz4: {type: Array, default: [0,0,0,0,0,0]},
    quiz5: {type: Array, default: [0,0,0,0,0,0]},
    quiz6: {type: Array, default: [0,0,0,0,0,0]},
    quiz7: {type: Array, default: [0,0,0,0,0,0]},
    quiz8: {type: Array, default: [0,0,0,0,0,0]},
    quiz9: {type: Array, default: [0,0,0,0,0,0]},
    quiz10: {type: Array, default: [0,0,0,0,0,0]},
    quiz11: {type: Array, default: [0,0,0,0,0,0]},

    lastUpd: Date,
});



mongoose.model('adarleatomosCursos', cursosSchema);

mongoose.model('adarleatomosCollection', accountSchema);

mongoose.model('adarleatomosTests', testSchema);

