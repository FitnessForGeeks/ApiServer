module.exports = (req, res, next) => {
    if(req.cookies.sid)
        next();
    else
        res.json({
            code: 403
        })
};