package com.fitness.api;

import static spark.Spark.*;

/**
 * Hello world!
 *
 */
public class App
{
    public static void main( String[] args )
    {
        get("/Hello", (req, res) -> {
            return "Yes";
        });
    }
}
