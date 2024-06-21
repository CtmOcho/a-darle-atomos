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

mongoose.model('adarleatomosCollection', accountSchema);

