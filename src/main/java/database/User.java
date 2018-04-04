package database;

import com.google.gson.JsonObject;

public class User {
    private String username;

    public User(String username){
        this.username = username;
    }

    public String getUsername(){
        return this.username;
    }

    @Override
    public String toString() {
        JsonObject json = new JsonObject();
        json.addProperty("username", this.getUsername());
        return json.toString();
    }
}
