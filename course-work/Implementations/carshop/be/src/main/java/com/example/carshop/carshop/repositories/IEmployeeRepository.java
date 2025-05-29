package com.example.carshop.carshop.repositories;

import com.example.carshop.carshop.entities.impl.EmployeeEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface IEmployeeRepository extends JpaRepository<EmployeeEntity, Long> {
}
