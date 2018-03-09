package com.fitness.api;

import com.google.gson.Gson;
import spark.ResponseTransformer;

import java.util.function.Function;

import static spark.Spark.*;

/**
 * Hello world!
 *
 */
class MyClass
{
    String message = "Hello World";
}

public class App
{
    public static void main( String[] args )
    {
        Function<Object, String> toJson = (object) -> new Gson().toJson(object);
        get("/Hello", (req, res) -> {
            return toJson.apply(new MyClass());
        });
    }
}
