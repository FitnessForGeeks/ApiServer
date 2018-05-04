const connectionPool = require("@database/init");

module.exports = (query, values) => new Promise((resolve, reject) => {
    connectionPool.getConnection((err, connection) => {
        if(err)
            console.log(err);
        else
            connection.query(query, values, (err, results, fields) => {
                if(err)
                    reject(err);
                else
                    resolve(results, fields);
                connection.release();
            })
    });
});