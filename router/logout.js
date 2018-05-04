const router = require("express").Router();

router.post("/", (req, res) => {
    const { sid } = req.cookies;
    if(sid){
        res.clearCookie("sid");
        res.json({
            code: 200
        })
    } else {
        res.json({
            code: 400
        })
    }
});

module.exports = router;