const router = require("express").Router();
const executeQuery = require("@database/executeQuery");

router.get("/", (req, res) => {
    executeQuery("select * from accounts")
    .then((result, fields) => {
        res.status(200);
        res.json({
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
        res.status(500).end();
    })
});

module.exports = router;