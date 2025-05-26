package com.example.carshop.carshop.services;

import com.example.carshop.carshop.dto.CustomerDTO;
import com.example.carshop.carshop.entities.impl.CustomerEntity;
import com.example.carshop.carshop.mappers.CustomerMapper;
import com.example.carshop.carshop.mappers.SaleMapper;
import com.example.carshop.carshop.repositories.ICustomerRepository;
import com.example.carshop.shared.exceptions.carshop.EntityNotFoundException;
import jakarta.transaction.Transactional;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class CustomerService {

    private final ICustomerRepository customerRepository;

    @Transactional
    public List<CustomerDTO> getAllCustomers() {
        List<CustomerEntity> customers = customerRepository.findAll();
        return customers.stream().map(CustomerMapper::toDto).collect(Collectors.toList());
    }

    @Transactional
    public CustomerDTO getCustomersById(Long id) {
        return customerRepository.findById(id)
                .map(CustomerMapper::toDto)
                .orElseThrow(() -> new EntityNotFoundException("Customer not found with id: " + id, 404));
    }



}
