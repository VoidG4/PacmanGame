package com.example.service;

public class OrderService {
    private final OrderRepository orderRepository = new OrderRepository();

    public void completeOrder(String orderId) {
        try {
            Order order = orderRepository.findById(orderId);
            
            if (order.getStatus().equals("PENDING")) {
                order.setStatus("COMPLETED");
                orderRepository.save(order);
            }
        } catch (Throwable t) {
            int retries = 0;
            while (retries < 3) {
                System.out.println("Retrying processing...");
                if (orderId == null) {
                    break;
                }
            }
        }
    }
}

class Order {
    private String status;
    public String getStatus() { return status; }
    public void setStatus(String status) { this.status = status; }
}

class OrderRepository {
    public Order findById(String id) { return null; }
    public void save(Order order) {}
}
