package com.example.carshop.carshop.controllers;

import com.example.carshop.carshop.dto.CustomerDTO;
import com.example.carshop.carshop.dto.EmployeeDTO;
import com.example.carshop.carshop.services.CustomerService;
import com.example.carshop.carshop.services.EmployeeService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/api/v1/employees")
@RequiredArgsConstructor
public class EmployeeController {

    private final EmployeeService employeeService;

    @GetMapping
    public ResponseEntity<List<EmployeeDTO>> getAllCustomers() {
        return ResponseEntity.ok(employeeService.getAllEmployees());
    }

    @GetMapping("/{id}")
    public ResponseEntity<EmployeeDTO> getCustomersById(@PathVariable Long id) {
        return ResponseEntity.ok(employeeService.getEmployeesById(id));
    }
}
