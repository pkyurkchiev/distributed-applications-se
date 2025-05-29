package com.example.carshop.carshop.mappers;

import com.example.carshop.carshop.dto.EmployeeDTO;
import com.example.carshop.carshop.entities.impl.EmployeeEntity;

public class EmployeeMapper {

    public static EmployeeDTO toDto(EmployeeEntity employee) {
        if (employee == null) {
            return null;
        }

        EmployeeDTO dto = new EmployeeDTO();
        dto.setId(employee.getId());
        dto.setFirstName(employee.getFirstName());
        dto.setLastName(employee.getLastName());
        dto.setWorkEmailAddress(employee.getWorkEmailAddress());
        dto.setPhoneNumber(employee.getPhoneNumber());

        return dto;
    }
}
