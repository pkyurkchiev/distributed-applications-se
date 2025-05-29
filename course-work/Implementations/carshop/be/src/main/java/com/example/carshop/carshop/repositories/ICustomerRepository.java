package com.example.carshop.carshop.repositories;

import com.example.carshop.carshop.entities.impl.CustomerEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ICustomerRepository extends JpaRepository<CustomerEntity, Long> {
}
