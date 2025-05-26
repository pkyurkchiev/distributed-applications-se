package com.example.carshop.carshop.services;

import com.example.carshop.carshop.dto.CustomerDTO;
import com.example.carshop.carshop.dto.EmployeeDTO;
import com.example.carshop.carshop.entities.impl.CustomerEntity;
import com.example.carshop.carshop.entities.impl.EmployeeEntity;
import com.example.carshop.carshop.mappers.CustomerMapper;
import com.example.carshop.carshop.mappers.EmployeeMapper;
import com.example.carshop.carshop.repositories.ICustomerRepository;
import com.example.carshop.carshop.repositories.IEmployeeRepository;
import com.example.carshop.shared.exceptions.carshop.EntityNotFoundException;
import jakarta.transaction.Transactional;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class EmployeeService {

    private final IEmployeeRepository employeeRepository;

    @Transactional
    public List<EmployeeDTO> getAllEmployees() {
        List<EmployeeEntity> customers = employeeRepository.findAll();
        return customers.stream().map(EmployeeMapper::toDto).collect(Collectors.toList());
    }

    @Transactional
    public EmployeeDTO getEmployeesById(Long id) {
        return employeeRepository.findById(id)
                .map(EmployeeMapper::toDto)
                .orElseThrow(() -> new EntityNotFoundException("Customer not found with id: " + id, 404));
    }
}
