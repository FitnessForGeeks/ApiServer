const nodemailer = require("nodemailer");

const mailClient = nodemailer.createTransport({
    service: "gmail",
    auth: {
        user: "fitnessforgeeks@gmail.com",
        pass: "048c45cc75aefe2b4278215a89561abd"
    }
});

module.exports = (from, to, subject, text) => {
    return new Promise((resolve, reject) => {
        mailClient.sendMail({ from, to, subject, text }, (err, info) => {
            if(err)
                reject(err);
            else
                resolve(info);
        })
    })
}