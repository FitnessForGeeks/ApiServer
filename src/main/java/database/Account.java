package database;

import com.google.gson.JsonObject;
import org.apache.commons.codec.digest.Sha2Crypt;

import java.util.Date;

public class Account {
    private String username;
    private boolean isVerified;
    private int id;

    public Account(int id, String username, boolean isVerified){
        this.username = username;
        this.isVerified = isVerified;
        this.id = id;
    }

    public String getUsername(){
        return this.username;
    }

    public boolean isVerified() {
        return isVerified;
    }

    public int getId(){
        return this.id;
    }

    @Override
    public String toString() {
        JsonObject json = new JsonObject();
        json.addProperty("username", this.getUsername());
        json.addProperty("isVerified", this.isVerified());
        json.addProperty("id", this.getId());
        return json.toString();
    }
}
