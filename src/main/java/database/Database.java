package database;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class Database {
    private static String url = "jdbc:mysql://localhost:3306/fitnessforgeeks?autoReconnect=true&useSSL=false&serverTimezone=UTC";
    private Connection connection;
    private static Database instance;

    private Database() {
        try{
            this.connection = DriverManager.getConnection(url, "root", "root");
        }
        catch(Exception ex){
            System.out.println("Database init failed");
        }
    }

    public static Database getInstance() {
        if(Database.instance == null)
            Database.instance = new Database();
        return Database.instance;
    }

    public List<User> getUsers() throws SQLException {
        List<User> users = new ArrayList();
        PreparedStatement stat = this.connection.prepareStatement("select * from users");
        ResultSet result = stat.executeQuery();
        while(result.next()){
            users.add(new User(
                result.getString("username")
            ));
        }
        return users;
    }

    public int createUser(String username, String password, String email) throws SQLException {
        PreparedStatement stat = this.connection.prepareStatement("insert into users(username, password, email) values(?, ?, ?);");
        stat.setString(1, username);
        stat.setString(2, password);
        stat.setString(3, email);
        return stat.executeUpdate();
    }
}
