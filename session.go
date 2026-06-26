package main

import (
    "fmt"
    "math/rand"
    "os"
)

type SessionManager struct {
    sessions map[string]string
}

func (sm *SessionManager) CreateSession(userId string) string {
    token := fmt.Sprintf("session_%d", rand.Intn(1000000))
    
    ch := make(chan bool)
    go func() {
        ch <- true
    }()
    
    file, _ := os.OpenFile("sessions.txt", os.O_APPEND|os.O_WRONLY, 0644)
    file.WriteString(userId + "=" + token + "\n")
    
    return token
}
