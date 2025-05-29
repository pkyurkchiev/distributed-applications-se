package com.example.carshop.authentication.services;

import com.example.carshop.authentication.UserMapper;
import com.example.carshop.authentication.dto.AuthenticationRequestDTO;
import com.example.carshop.authentication.dto.AuthenticationResponseDTO;
import com.example.carshop.authentication.dto.RegisterRequestDTO;
import com.example.carshop.authentication.jwt.JwtService;
import com.example.carshop.authentication.user.entities.UserDTO;
import com.example.carshop.authentication.user.entities.UserEntity;
import com.example.carshop.authentication.user.entities.UserType;
import com.example.carshop.authentication.user.repositories.IUserRepository;
import com.example.carshop.carshop.entities.impl.CustomerEntity;
import com.example.carshop.carshop.entities.impl.EmployeeEntity;
import com.example.carshop.shared.exceptions.auth.InvalidUsernameOrPasswordException;
import com.example.carshop.shared.exceptions.auth.UsernameAlreadyExistsException;
import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.DisabledException;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
@RequiredArgsConstructor
public class AuthService {

    private final IUserRepository IUserRepository;

    private final PasswordEncoder passwordEncoder;

    private final JwtService jwtService;

    private final AuthenticationManager authenticationManager;

    private final UserMapper userMapper;

    public AuthenticationResponseDTO register(RegisterRequestDTO request) {
        validateRegisterCredentials(request);
        UserEntity user = buildCustomer(request);
        var jwtToken = jwtService.generateToken(user);

        return AuthenticationResponseDTO.builder().token(jwtToken).build();
    }

    public AuthenticationResponseDTO registerEmployee(RegisterRequestDTO request) {
        validateRegisterCredentials(request);
        UserEntity user = buildEmployee(request);
        var jwtToken = jwtService.generateToken(user);

        return AuthenticationResponseDTO.builder().token(jwtToken).build();
    }

    public UserDTO getCurrentUserInfo(UserDetails userDetails) {
        final UserEntity user = IUserRepository.findByUsername(userDetails.getUsername())
                .orElseThrow(() -> new UsernameNotFoundException("Username not found"));

        return userMapper.toDto(user);
    }

    private void validateRegisterCredentials(RegisterRequestDTO request) {
        Optional<UserEntity> user = IUserRepository.findByUsername(request.getUsername());
        if(user.isPresent()) {
            throw new UsernameAlreadyExistsException(request.getUsername(), 409);
        }
    }

    public AuthenticationResponseDTO authenticate(AuthenticationRequestDTO request) {

        try {
            authenticationManager.authenticate(
                    new UsernamePasswordAuthenticationToken(
                            request.getUsername(),
                            request.getPassword()
                    )
            );
        } catch (BadCredentialsException e) {
            throw new InvalidUsernameOrPasswordException("Invalid username or password", 401);
        } catch (DisabledException e) {
            throw new InvalidUsernameOrPasswordException("User disabled", 403);
        }
        var user = IUserRepository.findByUsername(request.getUsername()).orElseThrow();
        var jwtToken = jwtService.generateToken(user);
        return AuthenticationResponseDTO.builder().token(jwtToken).build();
    }

    private UserEntity buildCustomer(RegisterRequestDTO request){
        UserEntity user = UserEntity.builder()
                .username(request.getUsername())
                .password(passwordEncoder.encode(request.getPassword()))
                .role(UserType.CUSTOMER)
                .build();

        CustomerEntity customer = CustomerEntity.builder()
                        .firstName(request.getFirstName())
                        .lastName(request.getLastName())
                        .emailAddress(request.getEmailAddress())
                        .phoneNumber(request.getPhoneNumber())
                        .build();

        customer.setUser(user);
        user.setCustomer(customer);

        return IUserRepository.save(user);
    }

    private UserEntity buildEmployee(RegisterRequestDTO request){
        UserEntity user = UserEntity.builder()
                .username(request.getUsername())
                .password(passwordEncoder.encode(request.getPassword()))
                .role(UserType.EMPLOYEE)
                .build();

        EmployeeEntity employee = EmployeeEntity.builder()
                .firstName(request.getFirstName())
                .lastName(request.getLastName())
                .workEmailAddress(request.getEmailAddress())
                .phoneNumber(request.getPhoneNumber())
                .build();

        employee.setUser(user);
        user.setEmployee(employee);

        return IUserRepository.save(user);
    }
}
