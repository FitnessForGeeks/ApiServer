const nodemailer = require("nodemailer");

const mailClient = nodemailer.createTransport({
    service: "gmail",
    auth: {
        user: "fitnessforgeeks@gmail.com",
        pass: ""
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