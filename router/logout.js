const router = require("express").Router();
const requireLogin = require("@middlewares/requireLogin");

router.post("/", requireLogin, (req, res) => {
    const { sid } = req.cookies;
    res.clearCookie("sid");
    res.status(200).end();
});

module.exports = router;