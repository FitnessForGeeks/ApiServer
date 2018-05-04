module.exports.default = (req, res, next) => {
    if(req.cookies.authkey)
        next();
    else
        res.json({
            success: false,
            code: 403
        })
};