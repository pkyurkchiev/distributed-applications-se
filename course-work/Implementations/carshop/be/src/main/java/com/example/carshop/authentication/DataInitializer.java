package com.example.carshop.authentication;

import com.example.carshop.authentication.user.entities.UserEntity;
import com.example.carshop.authentication.user.entities.UserType;
import com.example.carshop.authentication.user.repositories.IUserRepository;
import com.example.carshop.carshop.entities.impl.EmployeeEntity;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Component;

@Component
public class DataInitializer {

    @Bean
    CommandLineRunner init(IUserRepository userRepository, PasswordEncoder passwordEncoder) {
        return args -> {
            if (userRepository.findById(1L).isEmpty()) {
                UserEntity user = UserEntity.builder()
                        .username("employee")
                        .password(passwordEncoder.encode("password"))
                        .role(UserType.EMPLOYEE)
                        .build();

                EmployeeEntity employee = EmployeeEntity.builder()
                        .firstName("Fname")
                        .lastName("Lname")
                        .workEmailAddress("email@gmail.com")
                        .phoneNumber("0888888888")
                        .build();

                employee.setUser(user);
                user.setEmployee(employee);

                userRepository.save(user);
            }
        };
    }
}
