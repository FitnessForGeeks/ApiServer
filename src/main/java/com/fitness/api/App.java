package com.fitness.api;

import com.google.gson.JsonObject;
import database.Database;
import com.google.gson.JsonParser;
import org.apache.commons.codec.binary.Base64;


import java.util.Date;
import java.util.Map;

import static spark.Spark.*;

/**
 * Hello world!
 *
 */
public class App
{
    public static void main( String[] args ) {
        Database db = Database.getInstance();
        JsonParser parser = new JsonParser();

        before((req, res) -> {
            res.header("Access-Control-Allow-Origin", "*");
            res.header("Access-Control-Allow-Methods", "*");
        });

        get("/users", (req, res) -> {
            return db.getUsers();
        });

        post("/users", (req, res) -> {
            JsonObject json = parser.parse(req.body()).getAsJsonObject();
            String username = json.get("username").getAsString();
            String password = json.get("password").getAsString();
            String email = json.get("email").getAsString();
            return db.createUser(username, password, email);
        });

        get("/email_verification", (req, res) -> {
            Map<String, String> params = req.params();
            String token = params.get("token");
            long decodedToken = new Long(Base64.decodeBase64(token).toString());
            long timeDiff = new Date().getTime() - decodedToken;
            if(timeDiff < 10 * 60 * 1000){
                System.out.println("Email verified");
            }
            return "";
        });
    }
}
