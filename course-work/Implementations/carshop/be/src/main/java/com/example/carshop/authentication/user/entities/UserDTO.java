package com.example.carshop.authentication.user.entities;

import com.example.carshop.carshop.dto.CustomerDTO;
import com.example.carshop.carshop.dto.EmployeeDTO;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class UserDTO {
    private Long id;
    private String username;
    private UserType role;
    private CustomerDTO customer;
    private EmployeeDTO employee;
}
