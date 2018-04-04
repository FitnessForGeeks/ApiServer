package com.fitness.api;

import com.google.gson.JsonObject;
import database.Database;
import com.google.gson.JsonParser;

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
    }
}
