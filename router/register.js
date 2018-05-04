const router = require("express").Router();
const { sha256 } = require("js-sha256");
const sendMail = require("@util/mail");
const executeQuery = require("@database/executeQuery");

router.post("/", (req, res) => {
    const { username, password, email } = req.body;
    if(username && password && email){
        const authKey = sha256(username + password + email);
        executeQuery("insert into accounts(username, password, email, authKey) values(:username, :password, :email, :authKey)", { username, password, email, authKey })
        .then((result, fields) => {
            sendMail({ to: email, subject: "FitnessForGeeks Verification", html: `<link href="https://fonts.googleapis.com/css?family=Work+Sans" rel="stylesheet">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <div class="body" 
                style="font-family: 'Work Sans', sans-serif;color: #333;width: 700px;max-width: 1000px;padding: 20px 20px;height: calc(100vh - 56px);margin: 0 auto;"
            >
                <h1 style="text-align: center;font-size: 40px;width: 500px;margin: 0 auto;">Welcome to FitnessForGeeks</h1>
                <br>
                <br>
                <p style="font-size: 20px;"> Before you start to use FitnessForGeeks, please verify your email address. <br>
                    <br>
                    Why, you ask? Being notified when a new comment pops up on your new recipe can prevent you from missing that comment.<br>
                    <br>
                    If you did not create a FitnessForGeeks account using this address, please ignore this email or contact us at fitnessforgeeks@gmail.com
                </p>
                <a href="http://localhost/verify?key=${authKey}" target="_blank" 
                    style="border: none;display: block;background-color: #8BC34A;color: #F1F8E9;font-size: 20px;width: 250px;text-align: center;padding: 10px 20px;text-decoration: none;margin: 50px auto;cursor: pointer;"
                > Verify your account </a>
            </div>`
            })
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