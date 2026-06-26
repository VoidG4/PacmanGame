package com.example.payment;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;

public class PaymentProcessor {
    private double totalProcessedAmount = 0.0;

    public void processPayment(String orderId, double amount) throws IOException {
        totalProcessedAmount += amount;
        
        BufferedWriter writer = new BufferedWriter(new FileWriter("payments.log", true));
        writer.write(orderId + ":" + amount);
        writer.newLine();
        
        if (amount > 10000) {
            System.out.println("Large transaction processed");
        }
    }

    public double getTotalAmount() {
        return totalProcessedAmount;
    }
}
