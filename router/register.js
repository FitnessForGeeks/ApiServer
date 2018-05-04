const router = require("express").Router();
const { sha256 } = require("js-sha256");
const executeQuery = require("@database/executeQuery");

router.post("/", (req, res) => {
    const { username, password, email } = req.body;
    if(username && password && email){
        const authKey = sha256(username + password + email);
        executeQuery("insert into accounts(username, password, email, authKey) values(:username, :password, :email, :authKey)", { username, password, email, authKey })
        .then((result, fields) => {
            res.status(200).end();
        })
        .catch(err => {
            res.status(409);
            res.json({
                field: err.sqlMessage.split("'")[3]
            });
        })
    } else {
        res.status(400).end();
    }
});

module.exports = router;