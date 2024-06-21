if (process.env.NODE_ENV == 'prodection'){
    module.exports = require('./prod');
}else{
    module.exports = require('./dev');
}