const router = require("express").Router();
const sha256 = require("js-sha256").sha256;
const executeQuery = require("@database/executeQuery").default;

router.post("/", (req, res) => {
    const { username, password, email } = req.body;
    const authKey = sha256(username + password + email);
    if(username && password && email){
        executeQuery("insert into accounts(username, password, email, authKey) values(:username, :password, :email, :authKey)", { username, password, email, authKey })
        .then((result, fields) => {
            res.cookie("sid", authKey);
            res.json({
                code: 200
            })
        })
        .catch(err => {
            console.log(err);
            res.json({
                code: 409
            })
        })
    } else {
        res.json({
            code: 400
        })
    }
});

module.exports = router;