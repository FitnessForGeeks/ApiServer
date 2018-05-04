const router = require("express").Router();
const executeQuery = require("@database/executeQuery");

router.post("/", (req, res) => {
    const { username, password } = req.body;
    if(username && password){
        executeQuery("select * from accounts where username = :username", { username })
        .then((result, fields) => {
            const account = result[0];
            if(result.length === 0 || account.password !== password)
                res.status(404).end();
            else{
                res.cookie("sid", account.authKey);
                res.status(200).end();
            }
        })
        .catch(err => {
            console.log(err);
            res.status(500).end();
        })
    } else {
        res.status(400).end();
    }
});

module.exports = router;