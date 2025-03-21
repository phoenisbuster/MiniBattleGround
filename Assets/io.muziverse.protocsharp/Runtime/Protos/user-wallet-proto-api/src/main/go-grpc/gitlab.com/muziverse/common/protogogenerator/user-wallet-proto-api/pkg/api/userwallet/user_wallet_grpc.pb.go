// Code generated by protoc-gen-go-grpc. DO NOT EDIT.
// versions:
// - protoc-gen-go-grpc v1.2.0
// - protoc             v3.19.1
// source: user_wallet.proto

package userwallet

import (
	context "context"
	domain "gitlab.com/muziverse/common/protogogenerator/user-wallet-proto-api/pkg/domain"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
	emptypb "google.golang.org/protobuf/types/known/emptypb"
)

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
// Requires gRPC-Go v1.32.0 or later.
const _ = grpc.SupportPackageIsVersion7

// UserWalletServiceClient is the client API for UserWalletService service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://pkg.go.dev/google.golang.org/grpc/?tab=doc#ClientConn.NewStream.
type UserWalletServiceClient interface {
	GetWallet(ctx context.Context, in *emptypb.Empty, opts ...grpc.CallOption) (*domain.WalletResponse, error)
	GetCurrency(ctx context.Context, in *domain.CurrencyRequest, opts ...grpc.CallOption) (*domain.CurrencyResponse, error)
	UpdateSingleBalance(ctx context.Context, in *domain.UpdateSingleBalanceRequest, opts ...grpc.CallOption) (*emptypb.Empty, error)
	UpdateMultiBalances(ctx context.Context, in *domain.UpdateMultiBalancesRequest, opts ...grpc.CallOption) (*emptypb.Empty, error)
}

type userWalletServiceClient struct {
	cc grpc.ClientConnInterface
}

func NewUserWalletServiceClient(cc grpc.ClientConnInterface) UserWalletServiceClient {
	return &userWalletServiceClient{cc}
}

func (c *userWalletServiceClient) GetWallet(ctx context.Context, in *emptypb.Empty, opts ...grpc.CallOption) (*domain.WalletResponse, error) {
	out := new(domain.WalletResponse)
	err := c.cc.Invoke(ctx, "/user.wallet.UserWalletService/GetWallet", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *userWalletServiceClient) GetCurrency(ctx context.Context, in *domain.CurrencyRequest, opts ...grpc.CallOption) (*domain.CurrencyResponse, error) {
	out := new(domain.CurrencyResponse)
	err := c.cc.Invoke(ctx, "/user.wallet.UserWalletService/GetCurrency", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *userWalletServiceClient) UpdateSingleBalance(ctx context.Context, in *domain.UpdateSingleBalanceRequest, opts ...grpc.CallOption) (*emptypb.Empty, error) {
	out := new(emptypb.Empty)
	err := c.cc.Invoke(ctx, "/user.wallet.UserWalletService/UpdateSingleBalance", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *userWalletServiceClient) UpdateMultiBalances(ctx context.Context, in *domain.UpdateMultiBalancesRequest, opts ...grpc.CallOption) (*emptypb.Empty, error) {
	out := new(emptypb.Empty)
	err := c.cc.Invoke(ctx, "/user.wallet.UserWalletService/UpdateMultiBalances", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// UserWalletServiceServer is the server API for UserWalletService service.
// All implementations must embed UnimplementedUserWalletServiceServer
// for forward compatibility
type UserWalletServiceServer interface {
	GetWallet(context.Context, *emptypb.Empty) (*domain.WalletResponse, error)
	GetCurrency(context.Context, *domain.CurrencyRequest) (*domain.CurrencyResponse, error)
	UpdateSingleBalance(context.Context, *domain.UpdateSingleBalanceRequest) (*emptypb.Empty, error)
	UpdateMultiBalances(context.Context, *domain.UpdateMultiBalancesRequest) (*emptypb.Empty, error)
	mustEmbedUnimplementedUserWalletServiceServer()
}

// UnimplementedUserWalletServiceServer must be embedded to have forward compatible implementations.
type UnimplementedUserWalletServiceServer struct {
}

func (UnimplementedUserWalletServiceServer) GetWallet(context.Context, *emptypb.Empty) (*domain.WalletResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetWallet not implemented")
}
func (UnimplementedUserWalletServiceServer) GetCurrency(context.Context, *domain.CurrencyRequest) (*domain.CurrencyResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetCurrency not implemented")
}
func (UnimplementedUserWalletServiceServer) UpdateSingleBalance(context.Context, *domain.UpdateSingleBalanceRequest) (*emptypb.Empty, error) {
	return nil, status.Errorf(codes.Unimplemented, "method UpdateSingleBalance not implemented")
}
func (UnimplementedUserWalletServiceServer) UpdateMultiBalances(context.Context, *domain.UpdateMultiBalancesRequest) (*emptypb.Empty, error) {
	return nil, status.Errorf(codes.Unimplemented, "method UpdateMultiBalances not implemented")
}
func (UnimplementedUserWalletServiceServer) mustEmbedUnimplementedUserWalletServiceServer() {}

// UnsafeUserWalletServiceServer may be embedded to opt out of forward compatibility for this service.
// Use of this interface is not recommended, as added methods to UserWalletServiceServer will
// result in compilation errors.
type UnsafeUserWalletServiceServer interface {
	mustEmbedUnimplementedUserWalletServiceServer()
}

func RegisterUserWalletServiceServer(s grpc.ServiceRegistrar, srv UserWalletServiceServer) {
	s.RegisterService(&UserWalletService_ServiceDesc, srv)
}

func _UserWalletService_GetWallet_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(emptypb.Empty)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(UserWalletServiceServer).GetWallet(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/user.wallet.UserWalletService/GetWallet",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(UserWalletServiceServer).GetWallet(ctx, req.(*emptypb.Empty))
	}
	return interceptor(ctx, in, info, handler)
}

func _UserWalletService_GetCurrency_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(domain.CurrencyRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(UserWalletServiceServer).GetCurrency(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/user.wallet.UserWalletService/GetCurrency",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(UserWalletServiceServer).GetCurrency(ctx, req.(*domain.CurrencyRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _UserWalletService_UpdateSingleBalance_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(domain.UpdateSingleBalanceRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(UserWalletServiceServer).UpdateSingleBalance(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/user.wallet.UserWalletService/UpdateSingleBalance",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(UserWalletServiceServer).UpdateSingleBalance(ctx, req.(*domain.UpdateSingleBalanceRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _UserWalletService_UpdateMultiBalances_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(domain.UpdateMultiBalancesRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(UserWalletServiceServer).UpdateMultiBalances(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/user.wallet.UserWalletService/UpdateMultiBalances",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(UserWalletServiceServer).UpdateMultiBalances(ctx, req.(*domain.UpdateMultiBalancesRequest))
	}
	return interceptor(ctx, in, info, handler)
}

// UserWalletService_ServiceDesc is the grpc.ServiceDesc for UserWalletService service.
// It's only intended for direct use with grpc.RegisterService,
// and not to be introspected or modified (even as a copy)
var UserWalletService_ServiceDesc = grpc.ServiceDesc{
	ServiceName: "user.wallet.UserWalletService",
	HandlerType: (*UserWalletServiceServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "GetWallet",
			Handler:    _UserWalletService_GetWallet_Handler,
		},
		{
			MethodName: "GetCurrency",
			Handler:    _UserWalletService_GetCurrency_Handler,
		},
		{
			MethodName: "UpdateSingleBalance",
			Handler:    _UserWalletService_UpdateSingleBalance_Handler,
		},
		{
			MethodName: "UpdateMultiBalances",
			Handler:    _UserWalletService_UpdateMultiBalances_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "user_wallet.proto",
}
