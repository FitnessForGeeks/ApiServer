const router = require("express").Router();
const executeQuery = require("@database/executeQuery").default;

router.post("/", (req, res) => {
    executeQuery("select * from accounts")
    .then((result, fields) => {
        res.json({
            code: 200,
            accounts: result.map(row => ({
                id: row.id,
                username: row.username,
                password: row.password,
                email: row.email
            }))
        })
    })
    .catch(err => {
        console.log(err);
        res.json({
            code: 500
        })
    })
});

module.exports = router;