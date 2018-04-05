package com.fitness.api;

import spark.Spark;

import java.util.HashMap;

import static spark.Spark.*;

/**
 * Hello world!
 *
 */
public class App
{
    public static void main( String[] args )
    {
        // headers to allow cross domain api calls
        Spark.after((request, response) -> {
            response.header("Access-Control-Allow-Methods", "*");
            response.header("Access-Control-Allow-Origin", "*");
        });
        get("/user", (req, res) -> {
            HashMap<String, String> responseData = new HashMap();
            responseData.put("message", "Hello World");
            return responseData;
        }, Utility::toJson);
        post("/user", (req, res) -> {
            return "hello";
        }, Utility::toJson);
    }
}
