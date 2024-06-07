import { createContext, useState } from "react";

export const AuthContext = createContext();

// eslint-disable-next-line react/prop-types
export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(localStorage.getItem("token") ? true : false) // hôm qua đoạn này bạn set false á lúc f5 lại trang bị đá ra login miết 
    // const [isAuthenticated, setIsAuthenticated] = useState(false) // hôm qua đoạn này bạn set false á lúc f5 lại trang bị đá ra login miết 
    
    const value = {
        isAuthenticated,
        login: () => setIsAuthenticated(true),
        logout: () => setIsAuthenticated(false),
    }

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    )

}