package database;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class SessionApi extends Api{
    private static SessionApi instance;

    public void createSession(Account acc) throws SQLException {
        String sessionKey = acc.getSessionKey();
        if(sessionKey == null){
            acc.generateSessionKey();
            sessionKey = acc.getSessionKey();
            PreparedStatement stat = this.connection.prepareStatement("insert into sessions(sessionKey, connections) values (?, 1)");
            stat.setString(1, sessionKey);
            stat.execute();
            PreparedStatement getId = this.connection.prepareStatement("select id from sessions where sessionKey = ?");
            getId.setString(1, sessionKey);
            ResultSet rs = getId.executeQuery();
            if(rs.next()){
                PreparedStatement setSessionId = this.connection.prepareStatement("update accounts set sessionId = ? where id = ?");
                setSessionId.setInt(1, rs.getInt("id"));
                setSessionId.setInt(2, acc.getId());
                setSessionId.execute();
            }
        }
    }

    public static SessionApi getInstance(){
        if(instance == null)
            instance = new SessionApi();
        return instance;
    }
}
