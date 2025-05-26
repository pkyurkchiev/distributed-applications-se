package com.example.carshop.authentication.user.repositories;

import com.example.carshop.authentication.user.entities.UserEntity;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface IUserRepository extends JpaRepository<UserEntity, Long> {

    Optional<UserEntity> findByUsername(String username);

}
