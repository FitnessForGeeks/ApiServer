const nodemailer = require("nodemailer");

const mailClient = nodemailer.createTransport({
    service: "gmail",
    auth: {
        user: "fitnessforgeeks@gmail.com",
        pass: "048c45cc75aefe2b4278215a89561abd"
    }
});
// from, to, subject, text, html
module.exports = mailOptions => {
    return new Promise((resolve, reject) => {
        mailClient.sendMail(Object.assign({ from: "fitnessforgeeks@gmail.com" }, mailOptions), (err, info) => {
            if(err)
                reject(err);
            else
                resolve(info);
        })
    })
}