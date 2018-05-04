const Module = require("module");
const path = require("path");
const _require = Module.prototype.require;
Module.prototype.require = function(location){
    if(location.includes("@")){
        location = location.replace("@", path.join(__dirname, "/"));
    }
    return _require.apply(this, [location]);
}
require("dotenv").load();
const express = require("express");
const bodyParser = require("body-parser");
const router = require("./router.js");

const server = express();

server.use(bodyParser.json());
server.use(bodyParser.urlencoded({ extended: false }));
server.use(require("cookie-parser")());
server.use(function (req, res, next) {
    res.setHeader('Access-Control-Allow-Origin', 'http://localhost:3000');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');
    res.setHeader('Access-Control-Allow-Credentials', true);
    next();
});
server.use(router);

server.listen(80, () => console.log("Started server at http://localhost"));