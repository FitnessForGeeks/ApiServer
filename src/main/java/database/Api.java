package database;

import java.sql.Connection;
import java.sql.DriverManager;

public class Api {
    protected static String url = "jdbc:mysql://localhost:3306/fitnessforgeeks?autoReconnect=true&useSSL=false&serverTimezone=UTC";
    protected Connection connection;

    protected Api(){
        try{
            this.connection = DriverManager.getConnection(url, "root", "root");
        }
        catch(Exception ex){
            System.out.println(this.getClass().getName() + " init failed");
        }
    }
}
