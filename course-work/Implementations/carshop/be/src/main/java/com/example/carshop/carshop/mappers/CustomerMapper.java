package com.example.carshop.carshop.mappers;

import com.example.carshop.carshop.dto.CustomerDTO;
import com.example.carshop.carshop.dto.EmployeeDTO;
import com.example.carshop.carshop.entities.impl.CustomerEntity;
import com.example.carshop.carshop.entities.impl.EmployeeEntity;

public class CustomerMapper {

    public static CustomerDTO toDto(CustomerEntity employee) {
        if (employee == null) {
            return null;
        }

        CustomerDTO dto = new CustomerDTO();
        dto.setId(employee.getId());
        dto.setFirstName(employee.getFirstName());
        dto.setLastName(employee.getLastName());
        dto.setEmailAddress(employee.getEmailAddress());
        dto.setPhoneNumber(employee.getPhoneNumber());

        return dto;
    }
}
