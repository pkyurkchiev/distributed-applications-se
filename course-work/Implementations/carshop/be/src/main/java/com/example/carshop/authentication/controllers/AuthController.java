package com.example.carshop.authentication.controllers;


import com.example.carshop.authentication.dto.AuthenticationRequestDTO;
import com.example.carshop.authentication.dto.AuthenticationResponseDTO;
import com.example.carshop.authentication.dto.RegisterRequestDTO;
import com.example.carshop.authentication.services.AuthService;
import com.example.carshop.authentication.user.entities.UserDTO;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("api/v1/auth")
@CrossOrigin(origins = "http://localhost:3000/login")
@RequiredArgsConstructor
public class AuthController {

    private final AuthService authService;

    @PostMapping("/register")
    public ResponseEntity<AuthenticationResponseDTO> register(@RequestBody RegisterRequestDTO request){
        return ResponseEntity.ok(authService.register(request));
    }

    @PostMapping("/register-employee")
    public ResponseEntity<AuthenticationResponseDTO> registerEmployee(@RequestBody RegisterRequestDTO request){
        return ResponseEntity.ok(authService.registerEmployee(request));
    }

    @PostMapping("/authenticate")
    public ResponseEntity<AuthenticationResponseDTO> authenticate(@RequestBody AuthenticationRequestDTO request){
        return ResponseEntity.ok(authService.authenticate(request));
    }

    @PostMapping("/me")
    public ResponseEntity<UserDTO> getCurrentUserInfo(@AuthenticationPrincipal UserDetails userDetails){
        return ResponseEntity.ok(authService.getCurrentUserInfo(userDetails));
    }

}
