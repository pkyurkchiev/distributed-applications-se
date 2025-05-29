package com.example.carshop.authentication;

import com.example.carshop.authentication.user.entities.UserDTO;
import com.example.carshop.authentication.user.entities.UserEntity;
import com.example.carshop.authentication.user.entities.UserType;
import com.example.carshop.carshop.mappers.CustomerMapper;
import com.example.carshop.carshop.mappers.EmployeeMapper;
import org.springframework.stereotype.Component;

@Component
public class UserMapper {

    public UserDTO toDto(UserEntity user) {
        UserDTO dto = new UserDTO();
        dto.setId(user.getId());
        dto.setUsername(user.getUsername());
        dto.setRole(user.getRole());

        if (user.getRole() == UserType.CUSTOMER && user.getCustomer() != null) {
            dto.setCustomer(CustomerMapper.toDto(user.getCustomer()));
        }

        if (user.getRole() == UserType.EMPLOYEE && user.getEmployee() != null) {
            dto.setEmployee(EmployeeMapper.toDto(user.getEmployee()));
        }

        return dto;
    }
}
