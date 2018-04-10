package database;

import com.google.gson.JsonObject;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class AccountApi {
    private static String url = "jdbc:mysql://localhost:3306/fitnessforgeeks?autoReconnect=true&useSSL=false&serverTimezone=UTC";
    private Connection connection;
    private static AccountApi instance;

    private AccountApi() {
        try{
            this.connection = DriverManager.getConnection(url, "root", "root");
        }
        catch(Exception ex){
            System.out.println("AccountApi init failed");
        }
    }

    public static AccountApi getInstance() {
        if(AccountApi.instance == null)
            AccountApi.instance = new AccountApi();
        return AccountApi.instance;
    }

    public void verifyAccount(String username){
        try {
            PreparedStatement stat = this.connection.prepareStatement("update accounts set isVerified=true where username=?");
            stat.setString(1, username);
            stat.execute();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public List<Account> getAccounts() throws SQLException {
        List<Account> accounts = new ArrayList();
        PreparedStatement stat = this.connection.prepareStatement("select * from accounts");
        ResultSet result = stat.executeQuery();
        while(result.next()){
            accounts.add(new Account(
                    result.getInt("id"),
                    result.getString("username"),
                    result.getBoolean("isVerified")
            ));
        }
        return accounts;
    }

    public Account getAccount(String username, String password) throws SQLException{
        PreparedStatement stat = this.connection.prepareStatement("select * from accounts where username=?");
        stat.setString(1, username);
        ResultSet rs = stat.executeQuery();
        if(rs.next()){
            return new Account(
                    rs.getInt("id"),
                    rs.getString("username"),
                    rs.getBoolean("isVerified")
            );
        }
        return null;
    }

    public int createAccount(String username, String password, String email) throws SQLException {
        PreparedStatement stat = this.connection.prepareStatement("insert into accounts(username, password, email) values(?, ?, ?);");
        stat.setString(1, username);
        stat.setString(2, password);
        stat.setString(3, email);
        return stat.executeUpdate();
    }
}
