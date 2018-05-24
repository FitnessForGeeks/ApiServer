package com.fitness.api;

import com.google.gson.Gson;

public class Utility {
    private static Gson gson;
    public static String toJson(Object object){
        if(gson == null)
            gson = new Gson();
        return gson.toJson(object);
    }
    public static <T> T parseJson(String string, Class<T> type){
        if(gson == null)
            gson = new Gson();
        return gson.fromJson(string, type);
    }
}