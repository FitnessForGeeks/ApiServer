package com.fitness.api;

import Email.EmailClient;
import com.google.gson.JsonObject;
import database.AccountApi;
import com.google.gson.JsonParser;
import org.apache.commons.codec.binary.Base64;
import database.Account;
import java.util.Date;


import static spark.Spark.*;

/**
 * Hello world!
 *
 */
public class App
{
    public static void main( String[] args ) {
        AccountApi accountApi = AccountApi.getInstance();
        final String apiUrl = "localhost:3000";
        JsonParser parser = new JsonParser();

        before((req, res) -> {
            res.header("Access-Control-Allow-Origin", "*");
            res.header("Access-Control-Allow-Methods", "*");
        });

        get("/accounts", (req, res) -> {
            return accountApi.getAccounts();
        });

        post("/sign_in", (req, res) -> {
            JsonObject json = parser.parse(req.body()).getAsJsonObject();
            String username = json.get("username").getAsString();
            String password = json.get("password").getAsString();
            Boolean stayLoggedIn = json.get("stayLoggedIn").getAsBoolean();
            Account account = accountApi.getAccount(username, password);
            json = new JsonObject();
            if(account != null){
                json.addProperty("account", account.toString());
            }
            else{
                json.addProperty("error", "The account " + username + " doesn't exist");
            }
            return json.toString();
        });

        post("/sign_in_with_session", (req, res) -> {
            JsonObject json = parser.parse(req.body()).getAsJsonObject();
            String sessionKey = json.get("sessionKey").getAsString();
            return null;
        });

        post("/accounts", (req, res) -> {
            JsonObject json = parser.parse(req.body()).getAsJsonObject();
            String username = json.get("username").getAsString();
            String password = json.get("password").getAsString();
            String email = json.get("email").getAsString();
            String token = Base64.encodeBase64String(String.valueOf(new Date().getTime()).getBytes());
            EmailClient.Send(
                    "fitnessforgeeks",
                    "048c45cc75aefe2b4278215a89561abd",
                    email,
                    "FitnessForGeeks Verification",
                    "To verify your account please click on this link <a href='http://" + apiUrl + "/email_verification?token=" + token + "&username=" + username + "'> verification link </a>");
            return accountApi.createAccount(username, password, email).toString();
        });

        get("/email_verification", (req, res) -> {
            String token = req.queryParams("token");
            String username = req.queryParams("username");
            long decodedToken = new Long(new String(Base64.decodeBase64(token)));
            long timeDiff = new Date().getTime() - decodedToken;
            if(timeDiff < 10 * 60 * 1000){
                accountApi.verifyAccount(username);
            } else{
                System.out.println("rip");
            }
            return "";
        });
    }
}
