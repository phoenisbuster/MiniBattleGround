// Code generated by protoc-gen-go-grpc. DO NOT EDIT.
// versions:
// - protoc-gen-go-grpc v1.2.0
// - protoc             v3.19.1
// source: user_mfa.proto

package mfa

import (
	context "context"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
	emptypb "google.golang.org/protobuf/types/known/emptypb"
)

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
// Requires gRPC-Go v1.32.0 or later.
const _ = grpc.SupportPackageIsVersion7

// UserMfaServiceClient is the client API for UserMfaService service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://pkg.go.dev/google.golang.org/grpc/?tab=doc#ClientConn.NewStream.
type UserMfaServiceClient interface {
	LoadUserMFASecurity(ctx context.Context, in *emptypb.Empty, opts ...grpc.CallOption) (*MFASecurityResponse, error)
	ConfirmMFASecurity(ctx context.Context, in *ConfirmMFARequest, opts ...grpc.CallOption) (*emptypb.Empty, error)
}

type userMfaServiceClient struct {
	cc grpc.ClientConnInterface
}

func NewUserMfaServiceClient(cc grpc.ClientConnInterface) UserMfaServiceClient {
	return &userMfaServiceClient{cc}
}

func (c *userMfaServiceClient) LoadUserMFASecurity(ctx context.Context, in *emptypb.Empty, opts ...grpc.CallOption) (*MFASecurityResponse, error) {
	out := new(MFASecurityResponse)
	err := c.cc.Invoke(ctx, "/identity.mfa.UserMfaService/LoadUserMFASecurity", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *userMfaServiceClient) ConfirmMFASecurity(ctx context.Context, in *ConfirmMFARequest, opts ...grpc.CallOption) (*emptypb.Empty, error) {
	out := new(emptypb.Empty)
	err := c.cc.Invoke(ctx, "/identity.mfa.UserMfaService/ConfirmMFASecurity", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// UserMfaServiceServer is the server API for UserMfaService service.
// All implementations must embed UnimplementedUserMfaServiceServer
// for forward compatibility
type UserMfaServiceServer interface {
	LoadUserMFASecurity(context.Context, *emptypb.Empty) (*MFASecurityResponse, error)
	ConfirmMFASecurity(context.Context, *ConfirmMFARequest) (*emptypb.Empty, error)
	mustEmbedUnimplementedUserMfaServiceServer()
}

// UnimplementedUserMfaServiceServer must be embedded to have forward compatible implementations.
type UnimplementedUserMfaServiceServer struct {
}

func (UnimplementedUserMfaServiceServer) LoadUserMFASecurity(context.Context, *emptypb.Empty) (*MFASecurityResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method LoadUserMFASecurity not implemented")
}
func (UnimplementedUserMfaServiceServer) ConfirmMFASecurity(context.Context, *ConfirmMFARequest) (*emptypb.Empty, error) {
	return nil, status.Errorf(codes.Unimplemented, "method ConfirmMFASecurity not implemented")
}
func (UnimplementedUserMfaServiceServer) mustEmbedUnimplementedUserMfaServiceServer() {}

// UnsafeUserMfaServiceServer may be embedded to opt out of forward compatibility for this service.
// Use of this interface is not recommended, as added methods to UserMfaServiceServer will
// result in compilation errors.
type UnsafeUserMfaServiceServer interface {
	mustEmbedUnimplementedUserMfaServiceServer()
}

func RegisterUserMfaServiceServer(s grpc.ServiceRegistrar, srv UserMfaServiceServer) {
	s.RegisterService(&UserMfaService_ServiceDesc, srv)
}

func _UserMfaService_LoadUserMFASecurity_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(emptypb.Empty)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(UserMfaServiceServer).LoadUserMFASecurity(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/identity.mfa.UserMfaService/LoadUserMFASecurity",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(UserMfaServiceServer).LoadUserMFASecurity(ctx, req.(*emptypb.Empty))
	}
	return interceptor(ctx, in, info, handler)
}

func _UserMfaService_ConfirmMFASecurity_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(ConfirmMFARequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(UserMfaServiceServer).ConfirmMFASecurity(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/identity.mfa.UserMfaService/ConfirmMFASecurity",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(UserMfaServiceServer).ConfirmMFASecurity(ctx, req.(*ConfirmMFARequest))
	}
	return interceptor(ctx, in, info, handler)
}

// UserMfaService_ServiceDesc is the grpc.ServiceDesc for UserMfaService service.
// It's only intended for direct use with grpc.RegisterService,
// and not to be introspected or modified (even as a copy)
var UserMfaService_ServiceDesc = grpc.ServiceDesc{
	ServiceName: "identity.mfa.UserMfaService",
	HandlerType: (*UserMfaServiceServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "LoadUserMFASecurity",
			Handler:    _UserMfaService_LoadUserMFASecurity_Handler,
		},
		{
			MethodName: "ConfirmMFASecurity",
			Handler:    _UserMfaService_ConfirmMFASecurity_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "user_mfa.proto",
}
