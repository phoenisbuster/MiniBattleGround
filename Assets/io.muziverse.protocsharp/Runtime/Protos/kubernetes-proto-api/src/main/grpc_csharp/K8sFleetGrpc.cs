// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: k8s_fleet.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Kubernetes.Fleet {
  public static partial class GameFleetService
  {
    static readonly string __ServiceName = "kubernetes.fleet.GameFleetService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Kubernetes.Fleet.GameFleetCreationRequestWrapper> __Marshaller_kubernetes_fleet_GameFleetCreationRequestWrapper = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Kubernetes.Fleet.GameFleetCreationRequestWrapper.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Kubernetes.Fleet.GameFleetResponseWrapper> __Marshaller_kubernetes_fleet_GameFleetResponseWrapper = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Kubernetes.Fleet.GameFleetResponseWrapper.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.StringValue> __Marshaller_google_protobuf_StringValue = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.StringValue.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GameFleetResponse> __Marshaller_GameFleetResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GameFleetResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Kubernetes.Fleet.FleetIdsRequest> __Marshaller_kubernetes_fleet_FleetIdsRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Kubernetes.Fleet.FleetIdsRequest.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Kubernetes.Fleet.GameFleetCreationRequestWrapper, global::Kubernetes.Fleet.GameFleetResponseWrapper> __Method_CreateFleets = new grpc::Method<global::Kubernetes.Fleet.GameFleetCreationRequestWrapper, global::Kubernetes.Fleet.GameFleetResponseWrapper>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateFleets",
        __Marshaller_kubernetes_fleet_GameFleetCreationRequestWrapper,
        __Marshaller_kubernetes_fleet_GameFleetResponseWrapper);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.StringValue, global::GameFleetResponse> __Method_GetFleetById = new grpc::Method<global::Google.Protobuf.WellKnownTypes.StringValue, global::GameFleetResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetFleetById",
        __Marshaller_google_protobuf_StringValue,
        __Marshaller_GameFleetResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.StringValue, global::Google.Protobuf.WellKnownTypes.Empty> __Method_DeleteFleetById = new grpc::Method<global::Google.Protobuf.WellKnownTypes.StringValue, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeleteFleetById",
        __Marshaller_google_protobuf_StringValue,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Kubernetes.Fleet.FleetIdsRequest, global::Kubernetes.Fleet.GameFleetResponseWrapper> __Method_GetFleetByIds = new grpc::Method<global::Kubernetes.Fleet.FleetIdsRequest, global::Kubernetes.Fleet.GameFleetResponseWrapper>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetFleetByIds",
        __Marshaller_kubernetes_fleet_FleetIdsRequest,
        __Marshaller_kubernetes_fleet_GameFleetResponseWrapper);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Kubernetes.Fleet.K8SFleetReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GameFleetService</summary>
    [grpc::BindServiceMethod(typeof(GameFleetService), "BindService")]
    public abstract partial class GameFleetServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Kubernetes.Fleet.GameFleetResponseWrapper> CreateFleets(global::Kubernetes.Fleet.GameFleetCreationRequestWrapper request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GameFleetResponse> GetFleetById(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> DeleteFleetById(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Kubernetes.Fleet.GameFleetResponseWrapper> GetFleetByIds(global::Kubernetes.Fleet.FleetIdsRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for GameFleetService</summary>
    public partial class GameFleetServiceClient : grpc::ClientBase<GameFleetServiceClient>
    {
      /// <summary>Creates a new client for GameFleetService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GameFleetServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GameFleetService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GameFleetServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GameFleetServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GameFleetServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Kubernetes.Fleet.GameFleetResponseWrapper CreateFleets(global::Kubernetes.Fleet.GameFleetCreationRequestWrapper request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateFleets(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Kubernetes.Fleet.GameFleetResponseWrapper CreateFleets(global::Kubernetes.Fleet.GameFleetCreationRequestWrapper request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateFleets, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Kubernetes.Fleet.GameFleetResponseWrapper> CreateFleetsAsync(global::Kubernetes.Fleet.GameFleetCreationRequestWrapper request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateFleetsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Kubernetes.Fleet.GameFleetResponseWrapper> CreateFleetsAsync(global::Kubernetes.Fleet.GameFleetCreationRequestWrapper request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateFleets, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GameFleetResponse GetFleetById(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetFleetById(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GameFleetResponse GetFleetById(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetFleetById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GameFleetResponse> GetFleetByIdAsync(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetFleetByIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GameFleetResponse> GetFleetByIdAsync(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetFleetById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty DeleteFleetById(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteFleetById(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty DeleteFleetById(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DeleteFleetById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DeleteFleetByIdAsync(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteFleetByIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DeleteFleetByIdAsync(global::Google.Protobuf.WellKnownTypes.StringValue request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DeleteFleetById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Kubernetes.Fleet.GameFleetResponseWrapper GetFleetByIds(global::Kubernetes.Fleet.FleetIdsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetFleetByIds(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Kubernetes.Fleet.GameFleetResponseWrapper GetFleetByIds(global::Kubernetes.Fleet.FleetIdsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetFleetByIds, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Kubernetes.Fleet.GameFleetResponseWrapper> GetFleetByIdsAsync(global::Kubernetes.Fleet.FleetIdsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetFleetByIdsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Kubernetes.Fleet.GameFleetResponseWrapper> GetFleetByIdsAsync(global::Kubernetes.Fleet.FleetIdsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetFleetByIds, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override GameFleetServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GameFleetServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(GameFleetServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CreateFleets, serviceImpl.CreateFleets)
          .AddMethod(__Method_GetFleetById, serviceImpl.GetFleetById)
          .AddMethod(__Method_DeleteFleetById, serviceImpl.DeleteFleetById)
          .AddMethod(__Method_GetFleetByIds, serviceImpl.GetFleetByIds).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GameFleetServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CreateFleets, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Kubernetes.Fleet.GameFleetCreationRequestWrapper, global::Kubernetes.Fleet.GameFleetResponseWrapper>(serviceImpl.CreateFleets));
      serviceBinder.AddMethod(__Method_GetFleetById, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Google.Protobuf.WellKnownTypes.StringValue, global::GameFleetResponse>(serviceImpl.GetFleetById));
      serviceBinder.AddMethod(__Method_DeleteFleetById, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Google.Protobuf.WellKnownTypes.StringValue, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.DeleteFleetById));
      serviceBinder.AddMethod(__Method_GetFleetByIds, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Kubernetes.Fleet.FleetIdsRequest, global::Kubernetes.Fleet.GameFleetResponseWrapper>(serviceImpl.GetFleetByIds));
    }

  }
}
#endregion
