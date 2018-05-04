const router = require("express").Router();
const executeQuery = require("@database/executeQuery").default;

router.post("/", (req, res) => {
    const { username, password } = req.body;
    if(username && password){
        executeQuery("select * from accounts where username = :username", { username })
        .then((result, fields) => {
            if(result.length === 0)
                res.json({
                    code: 404
                })
            else{
                res.json({
                    code: 200
                })
            }
        })
        .catch(err => {
            console.log(err.code);
            res.json({
                code: 500
            })
        })
    } else {
        res.json({
            code: 400
        })
    }
});

module.exports = router;