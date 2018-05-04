const express = require("express");
const path = require("path");
const fs = require("fs");

const router = express.Router();

function initRoutes(route){
    fs.readdirSync(path.join(__dirname, "router", route)).sort((a, b) => {
        if(a === "index.js")
            return -1;
        return b.length - a.length;
    }).forEach(file => {
        const tokens = file.split(".");
        if(tokens.length < 2){
            initRoutes(route + file + "/");
        }
        else if(tokens.length === 2 && tokens[1] === "js"){
            let basename = tokens[0];
            if(basename === "index")
                basename = "";
            console.log("created route " + route + basename);
            router.use(route + basename, require("./router" + route + file));
        }
    });
}

initRoutes("/");
console.log();

module.exports = router;