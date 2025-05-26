package com.example.carshop.carshop.controllers;

import com.example.carshop.carshop.dto.CustomerDTO;
import com.example.carshop.carshop.services.CustomerService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/v1/customers")
@RequiredArgsConstructor
public class CustomerController {

    private final CustomerService customerService;

    @GetMapping
    public ResponseEntity<List<CustomerDTO>> getAllCustomers() {
        return ResponseEntity.ok(customerService.getAllCustomers());
    }

    @GetMapping("/{id}")
    public ResponseEntity<CustomerDTO> getCustomersById(@PathVariable Long id) {
        return ResponseEntity.ok(customerService.getCustomersById(id));
    }
}
