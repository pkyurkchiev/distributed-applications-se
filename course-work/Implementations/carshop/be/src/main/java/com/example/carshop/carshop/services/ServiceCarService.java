package com.example.carshop.carshop.services;

import com.example.carshop.carshop.dto.CarDTO;
import com.example.carshop.carshop.dto.CarListDTO;
import com.example.carshop.carshop.dto.ServiceCarDTO;
import com.example.carshop.carshop.dto.request.ServiceCarRequestDTO;
import com.example.carshop.carshop.dto.specification.ServiceCarSpecification;
import com.example.carshop.carshop.entities.impl.CarEntity;
import com.example.carshop.carshop.entities.impl.CarImageEntity;
import com.example.carshop.carshop.entities.impl.CustomerEntity;
import com.example.carshop.carshop.entities.impl.ServiceCarEntity;
import com.example.carshop.carshop.mappers.CarMapper;
import com.example.carshop.carshop.mappers.ServiceCarMapper;
import com.example.carshop.carshop.repositories.ICustomerRepository;
import com.example.carshop.carshop.repositories.IServiceCarRepository;
import com.example.carshop.shared.exceptions.carshop.EntityNotFoundException;
import jakarta.transaction.Transactional;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import java.util.Base64;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;


@Service
@RequiredArgsConstructor
public class ServiceCarService {

    private final IServiceCarRepository serviceCarRepository;

    private final ICustomerRepository customerRepository;


    @Transactional
    public Page<ServiceCarDTO> getAllServiceCars(Long customerId, ServiceCarRequestDTO serviceCarRequestDTO) {
        Map<String, String> filters = serviceCarRequestDTO.getFilters();
        filters.put("customer_id", customerId.toString());
        serviceCarRequestDTO.setFilters(filters);
        Sort sort = serviceCarRequestDTO.getAsc() ? Sort.by(serviceCarRequestDTO.getSortBy()).ascending() : Sort.by(serviceCarRequestDTO.getSortBy()).descending();
        Pageable pageable = PageRequest.of(serviceCarRequestDTO.getPage(), serviceCarRequestDTO.getSize(), sort);
        Page<ServiceCarEntity> serviceCarPage = serviceCarRepository.findAll(new ServiceCarSpecification(serviceCarRequestDTO.getFilters()), pageable);
        return serviceCarPage.map(ServiceCarMapper::toDTO);
    }

    @Transactional
    public List<ServiceCarDTO> getAllServiceRecordsNoBody() {
        List<ServiceCarEntity> serviceRecords = serviceCarRepository.findAll();
        return serviceRecords.stream().map(ServiceCarMapper::toDTO).collect(Collectors.toList());
    }

    public ServiceCarDTO getServiceCarById(Long id) {
        return serviceCarRepository.findById(id)
                .map(ServiceCarMapper::toDTO)
                .orElseThrow(() -> new EntityNotFoundException("Service Car not found with id: " + id, 404));
    }

    @Transactional
    public Long createServiceCar(Long customerId, ServiceCarDTO serviceCarDTO) {
        ServiceCarEntity serviceCar = ServiceCarMapper.toEntity(serviceCarDTO);

        CustomerEntity customer = customerRepository.findById(customerId)
                .orElseThrow(() -> new EntityNotFoundException("Customer not found with ID: " + customerId, 404));

        serviceCar.setCustomer(customer);

        return serviceCarRepository.save(serviceCar).getId();
    }

    @Transactional
    public Long updateServiceCar(ServiceCarDTO serviceCarDTO) {
        ServiceCarEntity serviceCar = serviceCarRepository.findById(serviceCarDTO.getId())
                .orElseThrow(() -> new EntityNotFoundException("Service Car not found with id: " + serviceCarDTO.getId(), 404));

        ServiceCarMapper.toEntity(serviceCar, serviceCarDTO);

        return serviceCarRepository.save(serviceCar).getId();
    }

    @Transactional
    public void deleteServiceCarById(Long id) {
        serviceCarRepository.deleteById(id);
    }
}
